/****** Object:  View [dbo].[vw_AvailableCourses]    Script Date: 12/02/2024 18:30:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE OR ALTER   VIEW [dbo].[vw_AvailableCourses]
AS
SELECT  course.Id
       ,course.CourseMasterId
       ,course.[Name]
	   ,course.[Description]
	   ,course.[CourseCategoryId]
	   ,course.[CourseCategoryName]
	   ,course.[RegistrationStartDate]
	   ,course.[RegistrationEndDate]
	   ,course.[StartDate]
	   ,course.[EndDate]
	   ,course.Capacity
	   ,sc.Registrations
FROM   dbo.vw_Course  course
 INNER JOIN
  (
    SELECT 
      c.[Id]
   ,COUNT(sc.CourseId) Registrations 
  FROM [dbo].[Course] c
LEFT JOIN CourseRegistration sc ON c.Id = sc.CourseId
GROUP BY c.Id
  )sc ON course.Id =sc.Id
  WHERE RegistrationEndDate > GETDATE()
  AND Capacity > sc.Registrations  
GO


/****** Object:  View [dbo].[vw_RegisteredCourses]    Script Date: 12/02/2024 23:03:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE OR ALTER       VIEW [dbo].[vw_RegisteredCourses]
AS
SELECT  
	   courseregistration.Id
	  ,course.Id CourseId
       ,course.CourseMasterId
       ,course.[Name]
	   ,course.[Description]
	   ,course.[CourseCategoryId]
	   ,course.[CourseCategoryName]
	   ,course.[RegistrationStartDate]
	   ,course.[RegistrationEndDate]
	   ,course.[StartDate]
	   ,course.[EndDate]
	   ,courseregistration.StudentId
	   ,courseregistration.RegisteredBy
	   ,courseregistration.RegistrationDate
	   
FROM   dbo.vw_Course  course
 INNER JOIN
  dbo.CourseRegistration courseregistration ON course.Id = courseregistration.CourseId
GO


/****** Object:  StoredProcedure [dbo].[usp_CourseRegistration_Insert]    Script Date: 12/02/2024 23:04:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE OR ALTER     PROC [dbo].[usp_CourseRegistration_Insert] 
@CourseId INT,
@StudentId INT,
@RegisteredBy NVARCHAR(100) ,
@RegistrationDate datetime 
AS
	SET NOCOUNT ON
	SET XACT_ABORT ON
	BEGIN TRY
	BEGIN TRANSACTION;

	INSERT INTO [dbo].[CourseRegistration]
           ([StudentId]
           ,[CourseId]
           ,[RegisteredBy]
           ,[RegistrationDate])
     SELECT
            @StudentId
           ,@CourseId
           ,@RegisteredBy
           ,@RegistrationDate

	

	DECLARE @CourseRegistrationId INT = SCOPE_IDENTITY();
	SELECT
		@CourseRegistrationId AS CourseRegistrationId;

	
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


