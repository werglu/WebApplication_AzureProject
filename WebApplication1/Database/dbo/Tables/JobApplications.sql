CREATE TABLE [dbo].[JobApplications] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [OfferId]          INT            NOT NULL,
    [FirstName]        NVARCHAR (MAX) NOT NULL,
    [LastName]         NVARCHAR (MAX) NOT NULL,
    [PhoneNumber]      NVARCHAR (MAX) NOT NULL,
    [EmailAddress]     NVARCHAR (MAX) NOT NULL,
    [ContactAgreement] BIT            NOT NULL,
    [CvUrl]            NVARCHAR (MAX) NULL,
    [JobOfferId]       INT            NULL,
    CONSTRAINT [PK_JobApplications] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_JobApplications_JobOffer_JobOfferId] FOREIGN KEY ([JobOfferId]) REFERENCES [dbo].[JobOffer] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_JobApplications_JobOfferId]
    ON [dbo].[JobApplications]([JobOfferId] ASC);

