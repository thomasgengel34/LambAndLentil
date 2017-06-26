CREATE TABLE [INGREDIENT].[Ingredient] (
    [ID]              INT            IDENTITY (1, 1) NOT NULL,
    [Description]     NVARCHAR (MAX) NULL,
    [IngredientsList] NVARCHAR (MAX) NULL,
    [Name]            NVARCHAR (50)  NOT NULL,
    [CreationDate]    DATETIME       NOT NULL,
    [ModifiedDate]    DATETIME       NOT NULL,
    [AddedByUser]     NVARCHAR (MAX) NULL,
    [ModifiedByUser]  NVARCHAR (MAX) NULL,
    [ShoppingList_ID] INT            NULL,

);


GO
CREATE NONCLUSTERED INDEX [IX_ShoppingList_ID]
    ON [INGREDIENT].[Ingredient]([ShoppingList_ID] ASC);

