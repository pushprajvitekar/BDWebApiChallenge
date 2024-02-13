# BDWebApiChallenge
Asp.Net Core 8 Web Api to demostrate ORM less and DDD approach 
Create a REST API in C#/.Net 8 that provides the following functionality:
- CRUD operations for students
- CRUD operations for courses:
o a course needs to be of a certain category.
o valid categories can be retrieved via an already existing 
3rd party category REST API
located at: https://6523c967ea560a22a4e8d725.mockapi.io/CourseCategories
o consider that additional categories might be added by the 3rd party at any time
- allows to manage the assignment of students to their courses
o a student can be assigned to multiple courses
o courses can be attended by multiple students

Additional constraints:
- access to the API shall be secured (authorized)
- the data store of the API shall be a SQL Server database
- don’t use Entity Framework or another ORM, we also want to see your SQL skills


# JWT token authentication and authorization
  # Roles
   - Admin
   - Student
   - CourseManager  
    

# Test Users
 - student1
 - student2
 - coursemanager1
 - coursemanager2
 - admin
 
# Fluent Migrator console app


