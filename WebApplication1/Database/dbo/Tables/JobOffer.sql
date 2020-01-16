CREATE TABLE [dbo].[JobOffer] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [JobTitle]    NVARCHAR (MAX) NOT NULL,
    [Salary]      INT            NOT NULL,
    [CompanyId]   INT            NULL,
    [Description] NVARCHAR (MAX) DEFAULT (N'') NOT NULL,
    [Location]    NVARCHAR (50) NULL,
    [ValidUntil]  DATETIME2 (7)  NULL,
    CONSTRAINT [PK_JobOffer] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_JobOffer_Companies_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Companies] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_JobOffer_CompanyId]
    ON [dbo].[JobOffer]([CompanyId] ASC);


GO
