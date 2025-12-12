# Modifications Base de Données - Gestion de Stock

## Résumé

Aucune modification structurelle n'est nécessaire dans la base de données pour la gestion de stock. La table `Medicament` contient déjà la colonne `stock` qui est utilisée pour suivre les quantités disponibles.

## Structure Actuelle

### Table Medicament

La table `Medicament` contient déjà les colonnes nécessaires :

- `id_medicament` (INT, PRIMARY KEY)
- `stock` (INT, NOT NULL, DEFAULT 0)
- `seuil_alerte` (INT, NOT NULL, DEFAULT 5)

### Table Commande

La table `Commande` contient :

- `id_commande` (INT, PRIMARY KEY)
- `statut` (NVARCHAR(50), DEFAULT 'En attente')
- `date_commande` (DATETIME, DEFAULT GETDATE())

### Table LigneCommande

La table `LigneCommande` contient :

- `id_ligne` (INT, PRIMARY KEY)
- `id_commande` (INT, FOREIGN KEY)
- `id_medicament` (INT, FOREIGN KEY)
- `quantite` (INT, NOT NULL)
- `prix_unitaire` (DECIMAL(10, 2), NOT NULL)

## Fonctionnement de la Gestion de Stock

### Lors de la Confirmation d'une Commande

Quand une commande passe au statut "Confirmée" :

1. Le système récupère toutes les lignes de commande (`LigneCommande`) associées
2. Pour chaque ligne, il diminue le stock du médicament correspondant :
   ```sql
   UPDATE Medicament
   SET stock = stock - @Quantite
   WHERE id_medicament = @IdMedicament
   ```
3. Le système vérifie que le stock disponible est suffisant avant de confirmer

### Lors de l'Annulation d'une Commande Confirmée

Si une commande confirmée est annulée :

1. Le système récupère toutes les lignes de commande
2. Pour chaque ligne, il restitue le stock :
   ```sql
   UPDATE Medicament
   SET stock = stock + @Quantite
   WHERE id_medicament = @IdMedicament
   ```

## Vérifications Recommandées

### Contraintes de Stock

Il est recommandé d'ajouter une contrainte CHECK pour s'assurer que le stock ne devient jamais négatif :

```sql
ALTER TABLE Medicament
ADD CONSTRAINT CHK_Stock_Positif CHECK (stock >= 0);
```

### Index pour Performance

Pour améliorer les performances lors des mises à jour de stock :

```sql
CREATE INDEX IX_Medicament_Stock ON Medicament(stock);
```

### Trigger Optionnel (Alternative)

Si vous préférez gérer le stock automatiquement via un trigger au lieu du code C# :

```sql
CREATE TRIGGER TR_UpdateStockOnCommandeConfirm
ON Commande
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Si le statut passe à "Confirmée"
    IF EXISTS (
        SELECT 1 FROM inserted i
        INNER JOIN deleted d ON i.id_commande = d.id_commande
        WHERE i.statut = 'Confirmée' AND d.statut != 'Confirmée'
    )
    BEGIN
        -- Diminuer le stock
        UPDATE m
        SET m.stock = m.stock - lc.quantite
        FROM Medicament m
        INNER JOIN LigneCommande lc ON m.id_medicament = lc.id_medicament
        INNER JOIN inserted i ON lc.id_commande = i.id_commande
        WHERE i.statut = 'Confirmée';
    END

    -- Si une commande confirmée est annulée
    IF EXISTS (
        SELECT 1 FROM inserted i
        INNER JOIN deleted d ON i.id_commande = d.id_commande
        WHERE i.statut = 'Annulée' AND d.statut = 'Confirmée'
    )
    BEGIN
        -- Restituer le stock
        UPDATE m
        SET m.stock = m.stock + lc.quantite
        FROM Medicament m
        INNER JOIN LigneCommande lc ON m.id_medicament = lc.id_medicament
        INNER JOIN deleted d ON lc.id_commande = d.id_commande
        WHERE d.statut = 'Confirmée';
    END
END;
```

**Note** : Le code C# gère déjà cette logique, donc le trigger n'est pas nécessaire sauf si vous préférez cette approche.

## Scripts SQL Recommandés

### 1. Ajouter la contrainte de stock positif

```sql
-- Vérifier d'abord si la contrainte existe
IF NOT EXISTS (
    SELECT * FROM sys.check_constraints
    WHERE name = 'CHK_Stock_Positif'
)
BEGIN
    ALTER TABLE Medicament
    ADD CONSTRAINT CHK_Stock_Positif CHECK (stock >= 0);
END
```

### 2. Créer l'index pour améliorer les performances

```sql
IF NOT EXISTS (
    SELECT * FROM sys.indexes
    WHERE name = 'IX_Medicament_Stock' AND object_id = OBJECT_ID('Medicament')
)
BEGIN
    CREATE INDEX IX_Medicament_Stock ON Medicament(stock);
END
```

### 3. Vérifier les données existantes

```sql
-- Vérifier qu'il n'y a pas de stocks négatifs
SELECT id_medicament, nom, stock
FROM Medicament
WHERE stock < 0;

-- Corriger les stocks négatifs si nécessaire
UPDATE Medicament
SET stock = 0
WHERE stock < 0;
```

## Conclusion

La base de données est déjà bien structurée pour la gestion de stock. Les modifications apportées dans le code C# permettent de :

- Diminuer automatiquement le stock lors de la confirmation d'une commande
- Restituer le stock lors de l'annulation d'une commande confirmée
- Vérifier la disponibilité du stock avant confirmation

Les scripts SQL recommandés ci-dessus sont optionnels mais amélioreront la robustesse et les performances du système.
