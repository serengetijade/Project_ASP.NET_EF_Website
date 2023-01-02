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

https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/d4ea26a2dba10de060145f0539ee92fd72373520/Scripts/Blog.js#L67-L96

Both AJAX calls point to a controller method that assigns values to the Message and CommentRef parameters. New comments will have a CommentRef of zero, and subcomments will have a value that is equal to the Id number of their parent comment. The method returns the object, as a Json object, back to the AJAX call. 

https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/d4ea26a2dba10de060145f0539ee92fd72373520/Blog/Controllers/CommentsController.cs#L174-L186

Upon successful completion of the AJAX call, another javascript method is called to render the comment values as html within the view.

## Asynchronously update the view with user input, AJAX, and JQuery to display comments and subcomments.
In order to display the new comments and subcomments without having to reload the page, the AJAX call, after successfully creating a new comment, calls another method to insert html into the document view. As the view loops through each model object to render the page, there is an inner loop to check to see if another comment has a CommentRef parameter whose value matches the CommentId. If that other comment matches, it is a sub comment and displays in the subcomment section. But that only happens after the page is refreshed the model is looped through again; JavaScript and JQuery take the Json result from the asyncCreateComment method, detailed above, and insert it into a jQuery statement that renders it as html and inserts it into the document. The location for each insertion is defined by the id/ref passed along when the AJAX method is called. Each comment has a unique #comment-Id html id name associated with it.  

https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/d6064bad2e2c9603a1e13b380321dc4b9c5b8a7c/Scripts/Blog.js#L98-L152

https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/d6064bad2e2c9603a1e13b380321dc4b9c5b8a7c/Blog/Controllers/CommentsController.cs#L174-L186

## Asynchronous Like/Dislike Buttons and Progress Bar
Like and Dislike buttons were added as part of the partial view, and AJAX was used to call methods that increment each property value. When the user clicks on each, each property (Likes or Dislikes) goes up by one. By using AJAX, the entire page doesn’t need to be reloaded after a button click. The functions connect to the html elements (buttons) through their onclick attribute, and pass in the id number of that comment when clicked.

https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/d6064bad2e2c9603a1e13b380321dc4b9c5b8a7c/Scripts/Blog.js#L8-L29

When triggered, the ajax functions above use a url to point to method(s) within the CommentsController. The IncrementLike and IncrementDislike methods each perform the same actions for their respective parameters: they add one to the Like/Dislike property (an integer) and pass the increased number of likes/dislikes as a list. That result is then used, after the AJAX call is done, in the jQuery methods to update the displayed like/dislike counter by changing the text of one element and the css of another.

https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/d6064bad2e2c9603a1e13b380321dc4b9c5b8a7c/Blog/Controllers/CommentsController.cs#L136-L161

## Progress Bar 
The bootstrap progress bar displays the number of likes as a percentage of the total votes.

https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/d6064bad2e2c9603a1e13b380321dc4b9c5b8a7c/Blog/Views/Comments/_Comment.cshtml#L37

Recall the javascript function above that asynchronously updates the width attribute. However, the initial  value of the width attribute is set by calling a custom LikeRatio method. The result of the method is rounded down and multiplied by 100, to represent a percentage. Because it is a method of the class object, it does not need to pass in the like/dislike values when it is called, it already has access to them.

https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/d6064bad2e2c9603a1e13b380321dc4b9c5b8a7c/Blog/Models/Comment.cs#L29-L34

## Confirm Delete Modal Popup
A popup was created and added to ask users to confirm that they wish to delete a comment. The modal was created using html and javascript - when the trash can button is clicked, the display style of the modal is toggled to show. CSS was used to style, center, and change the background of the modal; animation was added so that a warning border flashes around the modal. Javascript methods were also created to hide the modal when the cancel button is clicked. 

https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/d6064bad2e2c9603a1e13b380321dc4b9c5b8a7c/Scripts/Blog.js#L31-L37

After the user confirms the deletion, asynchronous javascript (AJAX) is used to call a controller method to delete the record from the database and JavaScript is used to remove the element from the view:
https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/d6064bad2e2c9603a1e13b380321dc4b9c5b8a7c/Scripts/Blog.js#L48-L60

The AJAX call points to a method in the controller that deletes the record from the database, and returns the object’s id. 
https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/d6064bad2e2c9603a1e13b380321dc4b9c5b8a7c/Blog/Controllers/CommentsController.cs#L164-L171

Finally, jQuery fades in and out another element to notify users that the comment has been deleted. 

## “Comment Moderator” Role and Default User
A “CommentModerator” class was added to expand the ApplicationUser class, two new parameters were added, and a seed method was added to create a new role and assign a default user to it. The seed method was then called from within the configuration file to run as part of the application upon startup. The seed method created a new role using RoleManager, a default user was defined using UserManager, and the default user was then assigned to that new role. 

https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/d6064bad2e2c9603a1e13b380321dc4b9c5b8a7c/Blog/Models/CommentModerator.cs#L48-L62

## Easy Login Button (Partial View)
A button was created to automatically log in as a specific user: the Comment Moderator default user. The button was put inside of a partial view to be used in specific views. The button was styled with CSS to appear at a fixed position on the page. The button was placed as the submit button within a Razor form that calls the LoginCommentModerator method within the AccountController, and passes in the AbsoluteUri.

https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/d6064bad2e2c9603a1e13b380321dc4b9c5b8a7c/Views/Shared/_LoginBtnCommentModerator.cshtml#L1-L5

The LoginCommentModerator used hard coded values (the username and password of the default user) to apply the SignInManager.PasswordSignInAsync method to log in as that default user. A switch statement was added based on various possible results of the sign in status. The method was added to the AccountController.cs file. 

https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/d6064bad2e2c9603a1e13b380321dc4b9c5b8a7c/Controllers/AccountController.cs#L544-L564

## Controller-based Dynamic View
Within the shared _Layout file, an if statement was added, so that a partial view will only be displayed when it meets defined conditions: 
- the page is using the Comments controller, and 
- the current user is not logged in

https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/d6064bad2e2c9603a1e13b380321dc4b9c5b8a7c/Views/Shared/_Layout.cshtml#L29-L32

As a result, only users who are not yet logged in, will see the Easy Login Button (created in an early section). The view is now dynamic and displays differently according to the controller it uses to render the view- any view that uses the “Comments” controller will display the partial. 

## Role-based Authorization for Dynamic Displays
In order to limit which elements are displayed, inline html code was added to restrict the display of elements based on the user’s role. Now, the delete button for comments will only be visible to users signed in with the CommentModerator role. 

https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/d6064bad2e2c9603a1e13b380321dc4b9c5b8a7c/Blog/Views/Comments/_Comment.cshtml#L32-L35

## Role-based Authorization to Restrict Access to Views
In addition to restricting access to the delete button, by using inline razor statements, C#’s built-in Identity capability was used to prevent any user that is not a CommentModerator from accessing certain pages: the Create, Edit, and Delete pages.  

https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/d6064bad2e2c9603a1e13b380321dc4b9c5b8a7c/Blog/Controllers/CommentsController.cs#L41-L45

## Custom Access Denied Page
A custom access denied page was added to redirect users that try to access restricted pages. A custom view was created and added to the project; it was styled with basic CSS to meet specifications. To implement the custom response, a method was added to the CommentModerator class to overload the built in HandleUnauthorizedRequest method of the AuthorizeAttribute class

This was done by creating a class that extends from the AuthorizeAttribute class. If the authorization request context returns negative, meaning the user is unauthorized, it redirects them to the custom view. 

https://github.com/serengetijade/Project_ASP.NET_EF_Website/blob/d6064bad2e2c9603a1e13b380321dc4b9c5b8a7c/Blog/Models/CommentModerator.cs#L48-L62

>"Cool posts and kind comments make friends. Think about others online."
–David Chiles