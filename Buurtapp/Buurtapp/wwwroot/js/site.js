// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function changeSorteer(dropdown) {
    switch (dropdown) {
        case '1':
            toon(document.getElementById("datum"))
            verberg(document.getElementById("views"))
            verberg(document.getElementById("likes"))
            break;
        case '2':
            verberg(document.getElementById("datum"))
            toon(document.getElementById("views"))
            verberg(document.getElementById("likes"))
            break;
        case '3':
            verberg(document.getElementById("datum"))
            verberg(document.getElementById("views"))
            toon(document.getElementById("likes"))
            break;
        default:
            console.log('we zijn hier');
    }

    document.getElementById("sorteerSoort").value = dropdown;
}


function toon(element) {
    element.classList.remove("verberg");
    element.classList.add("toon");
}

function verberg(element) {
    element.classList.remove("toon");
    element.classList.add("verberg");
}

function setSorteerSoort(value) {
    document.getElementById("Filter").options[value - 1].selected = true;
    changeSorteer('' + value);
}

function getSorteerSoort() {
    return document.getElementById("sorteerSoort").value;
}

function sorterenOp(value, pagina, filter) {
    window.location.href = "/Post/index?sorteerSoort=" + getSorteerSoort() + "&sorteerVolgorde=" + value + "&pagina=" + pagina + '&filter=' + (filter || '');
}

function like(id) {

    $.ajax({
        type: "GET",
        url: '/Likes/WijzigLike',
        data: { id },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc,
        error: errorFunc
    });
}

function successFunc(data, status) {
    if (data.toegevoegd == 1) {
        document.getElementById(`id-${data.postid}`).classList.add("liked");
        updatelikes(data.postid, true);
    }
    else {
        document.getElementById(`id-${data.postid}`).classList.remove("liked");
        updatelikes(data.postid, false);
    }
}

function errorFunc() {
    alert('error');
}

function updatelikes(value, ophogen) {
    var element = document.getElementById(`like-${value}`);
    var aantal = parseInt(element.innerText);
    element.innerText = ophogen ? aantal + 1 : aantal - 1;

}

function sendComment(id) {
    var commentContent = document.getElementById("commentContent").value;

    if (commentContent != "") {
        $.ajax({
            type: "POST",
            url: "/Post/CreateComment/",
            data: JSON.stringify({ Content: commentContent, PostId: id }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: getMessage,
            error: getMessage
        });

        $.ajax({
            type: "Get",
            url: "/Post/Details/" + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: getMessage,
            error: getMessage
        });

        window.location.reload();
    }
}

function sendSolution(id) {
    var solutionTitle = document.getElementById("solutionTitle").value;
    var solutionDescription = document.getElementById("solutionDescription").value;
    console.log(solutionTitle)
    console.log(solutionDescription)
    if (solutionTitle != "" && solutionDescription != "") {
        $.ajax({
            type: "POST",
            url: "/Post/CreateSolution/",
            data: JSON.stringify({ Title: solutionTitle, Description: solutionDescription, PostId: id }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: getMessage,
            error: getMessage
        });

    }
    window.location.reload();
}

function reportPost(id) {
    var reportPostDescription = document.getElementById("reportPostDescription").value;
    if (reportPostDescription != "") {
        $.ajax({
            type: "POST",
            url: "/Post/ReportPost/",
            data: JSON.stringify({ PostId: id, Description: reportPostDescription }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: getMessage,
            error: getMessage
        });
    }
}

function reportComment(id) {
    var reportCommentDescription = document.getElementById("reportCommentDescription").value;
    if (reportCommentDescription != "") {
        $.ajax({
            type: "POST",
            url: "/Post/ReportComment/",
            data: JSON.stringify({ CommentId: id, Description: reportCommentDescription }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: getMessage,
            error: getMessage
        });
    }
}

function reportSolution(id) {
    var reportSolutionDescription = document.getElementById("reportSolutionDescription").value;
    if (reportSolutionDescription != "") {
        $.ajax({
            type: "POST",
            url: "/Post/ReportSolution/",
            data: JSON.stringify({ SolutionId: id, Description: reportSolutionDescription }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: getMessage,
            error: getMessage
        });
    }
}

function getMessage(msg) {
    console.log(msg)
}
