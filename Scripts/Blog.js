/*------------------------------*/
/*---------- COMMENTS ----------*/
/*------------------------------*/

const { parseJSON } = require("jquery");

/* OnClick function(s) to call a method in the controller, then use that result to change the html text in the view. */
function incrementLike(id) {
    $.ajax({
        type: "POST",
        url: "/Comments/IncrementLike",
        data: { id: id },
    })
    .done(function (result) {
        $("#commentLikes-" + id).text(result.Data[0]);
        $("#commentProgress-" + id).css("width", result.Data[1].toPrecision(4)+ "%") ;
    })
}
function incrementDislike(id) {
    $.ajax({
        type: "POST",
        url: "/Comments/IncrementDislike",
        data: { id: id },
    })
    .done(function (result) {
        $("#commentDislikes-" + id).text(result.Data[0]);
        $("#commentProgress-" + id).css("width", result.Data[1].toPrecision(4) + "%");
    })
}

/*Sticky Confirm Delete modal popup*/
function openDeleteModal(id) {
    $("#deleteComment-"+id).show();
}
function closeDeleteModal(id) {
    $("#deleteComment-"+id).hide();
}
///*Close modal popup if user clicks outside the modal*/
//document.addEventListener("click",
//    function (event) {
//        if (event.target.matches("#deleteComment-" + id)) {
//            closeDeleteModal()
//        }
//    }
//);

//Onclick function within confirm delete modal to delete a comment
function deleteComment(id) {
    //AJAX function to point to a controller method that removes the DB record
    $.ajax({
        type: "POST",
        url: "/Comments/DeleteCommentConfirmed",
        data: { id: id },
    })
    //Use javascript and jquery to remove the comment element from the view, then show a confirmation.
    const comment = document.getElementById("comment-" + id);
    comment.remove();
    closeDeleteModal();
    $("#deleteCommentConfirmed-" + id).fadeIn("slow").delay(3000).fadeOut("slow");
}
                                              
//Create a comment modal popup
function commentModal(id) {
    $("#createComment-"+id).toggle();
}

//Create a new (asynchronous) comment - part 1
function asyncCreateComment(id, ref) {
    var message = document.getElementById("commentInput-" + id).value;
    //New comments:
    if (ref == null) {
        $.ajax({
            type: "POST",
            url: "/Comments/CreateComment", //see part 2 in CommentsController.cs 
            data: { message: message, id: id },
            success: function (result) {
                //alert("Success - the asyncCreateComment function was successful for a NEW or from a comment.");
                displayComment(result, id);
            }
        })
    }
    //Subcomments:
    //ref is only passed in from subcomments to provide the id of the main comment (ref).
    else {
        $.ajax({
            type: "POST",
            url: "/Comments/CreateComment", 
            data: { message: message, id: ref },
            success: function (result) {
                //alert("Success - the asyncCreateComment function was successful from a SUBcomment.");
                displayComment(result, ref);
            }
        })
    }
    commentModal(id);
}

function displayComment(result, id) {
    var CommentId = result.Data.CommentId;
    var CommentDate = Date(parseInt(result.Data.CommentDate)).substring(0, 21);
    var Author = result.Data.Author;
    var Message = result.Data.Message;
    var Likes = result.Data.Likes;
    var Dislikes = result.Data.Dislikes;

    var addSubcomment = document.getElementById("commentFlag-" + id);

    var Comment = '<div id="comment-' + CommentId + '">' +
            '<p><small>' + CommentDate + '</small>' +
                '<br />' + Message + 
                '<br />' + 
                '<button class="btn" onclick="incrementLike(' + CommentId + ') ">❤️</button> <span id="commentLikes -' + CommentId + '">' + Likes + '</span >' +
                '<button class="btn" onclick="incrementDislike(' + CommentId + ')">🤮</button> <span id="commentDislikes-' + CommentId + '">' + Dislikes + '</span>' +
                '<button class="btn-danger" onclick="commentModal(' + CommentId + ')">Reply</button>' +
                '<button class="btn" onclick="openDeleteModal(' + CommentId + ')">🗑️</button>' +
            '</p>' +

            //'<!--LIKE RATIO NEEDS ATTENTION-->' +
            '<div class="progress">' +
                '<div id="commentProgress-' + CommentId + '" class="progress-bar bg-danger" role="progressbar" style="width:@Math.Round(item.LikeRatio())%"></div>' +
            '</div>' +
            '<hr />' +

            //'<!--Confirm Delete modal popup-->' +
            '<div id="deleteComment-' + CommentId + '" class="deleteComment">' +
                '<div class="deleteCommentContainer center">' +
                    '<h1>CONFIRM DELETE!</h1>' +
                    '<p>' +
                        'Are you certain that you would like to delete?' +
                        '<br><small>-This action cannot be undone-</small>' +
                    '</p>' +
                    '<button class="" type="button" onclick="closeDeleteModal(' + CommentId + ')">Cancel</button>' +
                    '<button class="btn-danger" type="submit" onclick="deleteComment(' + CommentId + ')">DELETE</button>' +
                '</div>' +
            '</div>' +
        '</div>' +

        //'<!--Confirmation of deletion message: hidden by CSS-->' +
        '<div id="deleteCommentConfirmed-' + CommentId + '" class="deleteCommentConfirmed">' +
        '<p><b>The comment was deleted successfully ✓</b></p>' +
        '</div>' +

        //'<!--Create a subcomment-->' +
        '<div id="createComment-' + CommentId + '" class="createComment">' +
        '<form name="createSubcomment" id="createSubcommentForm" method="post" action="" onsubmit="">' +
        '<input id="commentInput-' + CommentId + '" class="createInput" type="text" name="Message" />' +
        '<button type="button" class="btn-danger" onclick="commentModal(' + CommentId + ')">Cancel</button>' +
        '<button type="submit" class="btn-danger" onclick="asyncCreateComment(' + CommentId + ')">Post Reply</button>' +
        '</form>' +
        '</div>';

    $(Comment).insertBefore(addSubcomment);
}
/*----------------------------------*/
/*---------- END COMMENTS ----------*/
/*----------------------------------*/


