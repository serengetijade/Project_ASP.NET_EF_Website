# Project_ASP.NET_EF_Website
This repo highlights my work on a C# project using MVC and Entity Framework to add functions to an existing website for a theatre company. The focus is to expand their blog section to allow comments, subcomments, image uploads, and a user role to moderate those comments. The work involed adding creating models, adding databases, and writing custom logic for the CRUD functionality. In addition to C# several other languages, and libraries, were used, including: JavaScript, AJAX, SQL, JSON, jQuery, and Bootstrap.

## Skills Implemented
- <b>Languages and Libraries:</b> C#, JavaScript, HTML, CSS, Bootstrap.
    - Javascript was used to create custom modal popups: a 'Confirm Delete' element pops up to ask users if they are sure they wish to delete a record.
    - jQuery DOM methods were applied to fade in/out confirmation messages after user action. 
    - AJAX was used to change views without having to reload the entire page: 
        - comment likes and dislikes were incremented after button click and the progress bar updated; 
        - new comments were displayed; 
        - deleted comments were removed from the view.
    - Bootstrap: several elements were encorporated, such as cards, buttons, style classes, status bars, and badges.
- <b>MVC and Entity Framework:</b> implemented the Model-View-Controller design pattern within Entity Framework to develop the web application and add more features to the website. Work on the blog section involved interacting with each part of the MVC, from creating new models to represent the comments, to customizing the view to properly display images, to adding several custom methods to the controller.
- <b>Partial Views:</b> In order to leverage chunks of code, partial views were added to reduce redundancy. In some cases, logic was added to dynamically display views based on the user's role.  
- <b>Razor:</b> To utilize C# code within the html views, razor syntax was added to access database entities and display content throughout mulitple documents. 
- <b>Agile project management:</b> created and submitted successive iterations of the app, adding features and functionality with each generation.
- <b>Azure Dev Ops:</b> collaborated with team members to plan and track code assignments.
- <b>Git and Visual Studio Version Control:</b> coordinated branching and push requests to record changes to the project over time and avoid code conflicts.
- <b>Google:</b> debugged code using multiply online resources, researched new techniques, and found appropriate libraries to encorporate.

## Sprint Overview
During a two-week sprint, team members were tasked with adding functionality to an existing Web App. I was responsible for uploading images to be used as blog photos, and creating a section for comments and sub comments with asynchronous like/dislike buttons. Full, custom styled, CRUD functionality was required. 
The sprint was real-world experience as part of a large, multi-person group project. 
Specific tasks were assigned by the sprint leader, to be completed within a certain timeframe and meeting set parameters.
- Sprint duration: 2 weeks
- 15 stories completed
- Daily stand-ups
- Weekly code retrospectives
- Discord for chat and troubleshooting

## Team Experience
An important part of working with a team is providing support when you can. During the sprint, I was able to help debug my teammates code and provide direction in a few instances when they weren't sure how to proceed. I helped resolve a javascript modal issue, set up a loop to display database content, debug a new method that involved adding code to pass in record id numbers, and some other small issues. At the end of the sprint, I also organized an extended code retrospective (outside of scheduled hours) to teach my team mates about the work I'd done that they were unfamiliar with, and invited them to present interesting concepts they had come across. 

# Code Summary
## Code First BlogPhoto class, database table, and CRUD pages
A code first approach was used to create a new class for [blog photo](https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/main/Blog/Models/BlogPhoto.cs#L8) objects. The database table and CRUD pages were scaffolded by creating a controller from this class, and using Entity Framework. Due to C#s naming convention, the name of the database table had to be manually changed from “BlogPhotoes” to “BlogPhotos”. This also required updating the DbSet within the Models/IndentityModels file (a DbSet is the collection of entities in the database).

https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/4a5da55471a204024df5d4685a12a49ed418249a/Blog/Models/BlogPhoto.cs#L8-L13

In the newly created BlogPhotosController, a custom CreateBlogPhoto method was added to upload files to the database. Logic was added to ensure the uploaded files were not empty, and also of extension type .jpg, .jpeg, or .png. Alerts were added to notify users if each condition was not met. If the upload was valid, it was converted to a byte array and saved to the database as a new record (object). 

[BlogPhotosController:](https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/main/Blog/Controllers/BlogPhotosController.cs)
https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/4a5da55471a204024df5d4685a12a49ed418249a/Blog/Controllers/BlogPhotosController.cs#L142-L174

To convert the uploaded file to a byte array, another method was written utilizing BinaryReader. Then when the file needs to be rendered as an image, it is converted back using the built-in File method. 
[BlogPhotosController:](https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/main/Blog/Controllers/BlogPhotosController.cs)
https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/4a5da55471a204024df5d4685a12a49ed418249a/Blog/Controllers/BlogPhotosController.cs#L176-L194

## Custom Index/Edit/Delete View to Render Images
In order to render the uploaded blog photos on the [index](https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/main/Blog/Views/BlogPhotos/Index.cshtml#L91) page, the default template code was replaced with customized HTML and CSS. Each record (each uploaded blog photo) was looped through and displayed as a unique card with CSS hover effects and links to Details, Edit, and Delete pages. In order for the images to render, the Url.Action helper method was utilized to call the byteToImage method, described above, to convert the byte array back to an image so that it correctly renders in the view. The Details, Edit, and Delete pages were likewise adjusted to be able to render the image, so instead of the long, intelligible byte array, the image is displayed. 

Replace the default links with Url.Action references: 
https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/d4ea26a2dba10de060145f0539ee92fd72373520/Blog/Views/BlogPhotos/Index.cshtml#L91

Call the byteToImage method to convert the byte array to an image:
https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/4a5da55471a204024df5d4685a12a49ed418249a/Blog/Views/BlogPhotos/Index.cshtml#L92

## Code First BlogComments class, database, and CRUD pages 
A new model was defined to represent user comments and store them within a table in the database. The class constructor was modified so the DateTime property sets the current time- it will always record the time the comment was posted. Entity Framework scaffolding was used by creating a CommentsController. The CRUD pages were later modified and customized to expand their default functions. 

https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/d4ea26a2dba10de060145f0539ee92fd72373520/Blog/Models/Comment.cs#L9-L27

## Reorder the model by property value
The model’s controller was modified to reorder the list of records. Blog comments were to appear according to the comment date, one of the model’s properties, in order from newest to oldest. The Controller’s Index method was modified to pass along the list of database objects according to that property value, in descending order. 

```
CommentsController From This: 
        public ActionResult Index()
        {
            return View(db.Comments.ToList());
        }
 
To This: 
        public ActionResult Index()
        {
            return View(db.Comments.OrderByDescending(Comment => Comment.CommentDate).ToList());
        }
```
## Character Limit Helper Method
A static method was written to take in a string and an integer, then return a shortened version of the string. The string is truncated to the length of the given integer- it represents how many characters are allowed before cutting off the string. A ( . . . ) is concatenated to the end to indicate the string has been cut off.

https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/d4ea26a2dba10de060145f0539ee92fd72373520/Helpers/TextHelpers.cs#L12-L15

The method uses a ternary operator to determine if the string is already less than the given number, and if so, it returns the whole string. Otherwise, it returns a substring starting at index zero, through the provided integer index position.

## Customized Display Using Partial Views
A partial view was created to display the comments with customized html and css styling. The default Entity Framework layout of the model’s Index page was replaced with a customized design to meet established specifications. This partial can be used on other pages to display Comments in a stylized way- 
- Timespan and DateTime types were applied to display the time that has passed since the comment was uploaded.
- Images and buttons were added to show the number of Likes and Dislikes
- A bootstrap progress bar was added to show the ratio of likes to total votes. 
- An iconized delete button (for admins) added the ability to delete comments.  

## Create New Record From User Input
In order to have any [comments](https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/main/Blog/Views/Comments/_Comment.cshtml#L113-L118), users must be able to interact with the page and add their input. An html input was added to receive user input, and JavaScript was added to toggle the element as create, cancel, or post buttons are clicked. When the reply button is clicked, the popup appears and displays the input where users can enter a subcomment. Subcomments also include the reply popup so users can add replies to the parent comment from there, or from the parent comment directly.

New Comments: pass in 0
https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/d4ea26a2dba10de060145f0539ee92fd72373520/Blog/Views/Comments/_Comment.cshtml#L3-L11

Sub Comments: pass in the parent comment's id
https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/d4ea26a2dba10de060145f0539ee92fd72373520/Blog/Views/Comments/_Comment.cshtml#L61-L66

Sub Comments from within Sub Comments: pass in the parent comment's id AND the subcomment's id
https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/d4ea26a2dba10de060145f0539ee92fd72373520/Blog/Views/Comments/_Comment.cshtml#L113-L118

Asynchronous JavaScript, AJAX, was used to call a method to create a database record. The call passes the id, if any, of the comment and the reference (ref) to the parent comment, if any. 
### New Comments:
To create new comments, an id of 0 is passed in with the call and the ref is null: The ref value of null triggers the if statement for new comments. The id of 0 is used to set the CommentRef and NOT the commentId (that is auto-generated). A CommentRef of 0 means it is a new comment and is a subcomment, aka a reply to another comment. The null ref is also used to assign a value to the new comments CommentRef property, but being null, does NOT change the value of the controller, which is zero. That zero is then used as a reference to a location at the top of the comments section, where an element’s id was hard-coded to be #comment-0, so new comments will always display there.
### Subcomments:
Subcomments pass in their Id number as well as the reference to the parent comment. The Id number is used to determine the correct input element to pull from, and later where to display the subcomment. Unlike with new comments, a ref that is not null triggers a slightly different AJAX call that assigns the CommentRef parameter value to be that of the parent comment, indicating that it is a subcomment.  
