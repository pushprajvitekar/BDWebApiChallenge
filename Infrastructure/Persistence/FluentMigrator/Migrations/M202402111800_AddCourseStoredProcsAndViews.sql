/****** Object:  StoredProcedure [dbo].[usp_CourseCategory_Insert]    Script Date: 11/02/2024 18:17:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE OR ALTER     PROC [dbo].[usp_CourseCategory_Insert] @Id INT,
@Name NVARCHAR(1000)
AS
	SET NOCOUNT ON
	
		IF NOT EXISTS (SELECT
					Id
				FROM dbo.CourseCategory cc
				WHERE Id = @Id)
		BEGIN
			INSERT INTO dbo.CourseCategory (Id, Name)
				SELECT
					@Id
				   ,@Name
		END
	
	RETURN 0
GO


/****** Object:  StoredProcedure [dbo].[usp_CourseMaster_Insert]    Script Date: 11/02/2024 18:17:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE OR ALTER         PROC [dbo].[usp_CourseMaster_Insert] 
@Name NVARCHAR(1000) ,
@Description NVARCHAR(2000) =NULL ,
@CourseCategoryId INT ,
@CourseCategoryName NVARCHAR(1000) =NULL ,
@CreatedBy NVARCHAR(100) ,
@CreatedDate datetime 

AS
	SET NOCOUNT ON
	SET XACT_ABORT ON
	BEGIN TRY
	BEGIN TRANSACTION;
	IF (@CourseCategoryName IS NOT NULL AND @CourseCategoryId > 0)
	BEGIN

		EXEC dbo.usp_CourseCategory_Insert @Id = @CourseCategoryId
										  ,@Name = @CourseCategoryName
	END

	INSERT INTO dbo.CourseMaster (Name, Description, CourseCategoryId,CreatedBy, CreatedDate)
		SELECT

			@Name
		   ,@Description
		   ,@CourseCategoryId
		   ,@CreatedBy
		   ,@CreatedDate
	
  --Use a temp variable to ensure the correct data type
    DECLARE @CourseMasterId INT = SCOPE_IDENTITY();
    SELECT @CourseMasterId AS CourseMasterId;
	COMMIT TRANSACTION;
	END TRY
	BEGIN CATCH
	 SELECT   
        ERROR_NUMBER() AS ErrorNumber  
        ,ERROR_SEVERITY() AS ErrorSeverity  
        ,ERROR_STATE() AS ErrorState  
        ,ERROR_PROCEDURE() AS ErrorProcedure  
        ,ERROR_LINE() AS ErrorLine  
        ,ERROR_MESSAGE() AS ErrorMessage;   

    IF (XACT_STATE()) = -1  
    BEGIN  
        PRINT  
            N'The transaction is in an uncommittable state.' +  
            'Rolling back transaction.'  
        ROLLBACK TRANSACTION;  
    END;  

    IF (XACT_STATE()) = 1  
    BEGIN  
        PRINT  
            N'The transaction is committable.' +  
            'Committing transaction.'  
        COMMIT TRANSACTION;     
    END;  
	END CATCH
GO

/****** Object:  StoredProcedure [dbo].[usp_CourseMaster_Update]    Script Date: 11/02/2024 21:01:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROC [dbo].[usp_CourseMaster_Update]
@Id int,
@Name nvarchar(1000),
@Description nvarchar(2000)=NULL,
@CourseCategoryId INT,
@CourseCategoryName NVARCHAR(1000) =NULL 
AS 
    SET NOCOUNT ON
    SET XACT_ABORT ON

    BEGIN TRAN
	IF (@CourseCategoryName IS NOT NULL AND @CourseCategoryId > 0)
	BEGIN

		EXEC dbo.usp_CourseCategory_Insert @Id = @CourseCategoryId
										  ,@Name = @CourseCategoryName
	END
    UPDATE dbo.CourseMaster
    SET    Name = @Name, Description = @Description, CourseCategoryId = @CourseCategoryId
    WHERE  Id = @Id

  SELECT @Id 
    COMMIT
GO



/****** Object:  StoredProcedure [dbo].[usp_Course_Insert]    Script Date: 11/02/2024 21:00:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER   PROC [dbo].[usp_Course_Insert] 
@CourseMasterId INT,
@RegistrationStartDate DATETIME,
@RegistrationEndDate DATETIME,
@StartDate DATETIME,
@EndDate DATETIME,
@Capacity INT,
@CreatedBy NVARCHAR(100) ,
@CreatedDate datetime 
AS
	SET NOCOUNT ON
	SET XACT_ABORT ON
	BEGIN TRY
	BEGIN TRANSACTION;

	INSERT INTO dbo.Course (CourseMasterId, RegistrationStartDate,RegistrationEndDate, StartDate, EndDate, Capacity, CreatedBy, CreatedDate)
		SELECT
			@CourseMasterId
			,@RegistrationStartDate
			,@RegistrationEndDate
		   ,@StartDate
		   ,@EndDate
		   ,@Capacity
		   ,@CreatedBy
		  ,@CreatedDate 

	DECLARE @CourseId INT = SCOPE_IDENTITY();
	SELECT
		@CourseId AS CourseId;

	
	COMMIT TRANSACTION;   
	END TRY
	BEGIN CATCH
	 SELECT   
        ERROR_NUMBER() AS ErrorNumber  
        ,ERROR_SEVERITY() AS ErrorSeverity  
        ,ERROR_STATE() AS ErrorState  
        ,ERROR_PROCEDURE() AS ErrorProcedure  
        ,ERROR_LINE() AS ErrorLine  
        ,ERROR_MESSAGE() AS ErrorMessage;   

    IF (XACT_STATE()) = -1  
    BEGIN  
        PRINT  
            N'The transaction is in an uncommittable state.' +  
            'Rolling back transaction.'  
        ROLLBACK TRANSACTION;  
    END;  

    IF (XACT_STATE()) = 1  
    BEGIN  
        PRINT  
            N'The transaction is committable.' +  
            'Committing transaction.'  
        COMMIT TRANSACTION;     
    END;  
	END CATCH
GO

/****** Object:  StoredProcedure [dbo].[usp_CourseMaster_Update]    Script Date: 11/02/2024 21:02:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROC [dbo].[usp_Course_Update]
@Id int,
@CourseMasterId INT,
@RegistrationStartDate DATETIME,
@RegistrationEndDate DATETIME,
@StartDate DATETIME,
@EndDate DATETIME,
@Capacity INT
AS 
    SET NOCOUNT ON
    SET XACT_ABORT ON

    BEGIN TRAN
	
    UPDATE [dbo].[Course]
   SET 
       [RegistrationStartDate] = @RegistrationStartDate
      ,[RegistrationEndDate] = @RegistrationEndDate
      ,[StartDate] = @StartDate
      ,[EndDate] = @EndDate
      ,[Capacity] = @Capacity
 WHERE Id=@Id

  SELECT @Id 
    COMMIT
GO





/****** Object:  View [dbo].[vw_CourseMaster]    Script Date: 11/02/2024 18:21:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER VIEW [dbo].[vw_CourseMaster]
--WITH ENCRYPTION, SCHEMABINDING, VIEW_METADATA
AS
SELECT
	CM.Id
   ,CM.Name
   ,CM.Description
   ,CC.Id CourseCategoryId
   ,CC.Name CourseCategoryName
FROM dbo.CourseCategory CC
INNER JOIN dbo.CourseMaster CM
	ON CC.Id = CM.CourseCategoryId
--WITH CHECK OPTION
GO




/****** Object:  View [dbo].[vw_Course]    Script Date: 11/02/2024 18:22:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER VIEW [dbo].[vw_Course]
WITH  SCHEMABINDING
AS
	SELECT 
	 c.Id
	,cm.Id CourseMasterId
	,cm.Name [Name]
	,cm.Description [Description]
	,cc.Id CourseCategoryId
	,cc.Name CourseCategoryName
	,c.RegistrationStartDate
	,c.RegistrationEndDate
	,c.StartDate
	,c.EndDate
	,c.Capacity
	FROM dbo.Course c INNER JOIN dbo.CourseMaster cm ON c.Id = cm.Id
	INNER JOIN dbo.CourseCategory cc ON cm.CourseCategoryId = cc.Id
	--WITH CHECK OPTION
GO