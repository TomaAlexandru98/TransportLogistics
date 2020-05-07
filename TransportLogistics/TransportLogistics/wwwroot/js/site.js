function loadServerPartialView(container, baseUrl) {
    $.get(baseUrl, function (responseData) {
        $(container).html(responseData);
    });
}
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function Validate() {
    var correctForm = true;
    var name = document.getElementById("Name");
    if (document.getElementById("Name").value == "") {
        document.getElementById("LabelName").innerHTML = "The name is required";
        document.getElementById("LabelName").style.color = "red";
        //name.value =
        correctForm = false;
    }
    else {
        document.getElementById("LabelName").innerHTML = "Name";
        document.getElementById("LabelName").style.color = "green";
    }
    if (document.getElementById("Email").value == "") {
        document.getElementById("LabelEmail").innerHTML = "The email is required";
        document.getElementById("LabelEmail").style.color = "red";
        correctForm = false;
    }
    else  {
        document.getElementById("LabelEmail").innerHTML = "Email";
        document.getElementById("LabelEmail").style.color = "green";
    }
    if (document.getElementById("Role").value == "") {
        document.getElementById("LabelRole").innerHTML = "The role is required";
        document.getElementById("LabelRole").style.color = "red";
        correctForm = false;
    }
    else {
        document.getElementById("LabelRole").innerHTML = "Role";
        document.getElementById("LabelRole").style.color = "green";
    }
    if (document.getElementById("PhoneNumber").value == "") {
        document.getElementById("LabelPhoneNumber").innerHTML = "The phone number is required";
        document.getElementById("LabelPhoneNumber").style.color = "red";
        correctForm = false;
    }
    else {
        document.getElementById("LabelPhoneNumber").innerHTML = "Phone Number";
        document.getElementById("LabelPhoneNumber").style.color = "green";
    }
    if (correctForm == false) {
         
        $("#buttonSubmit").prop('disabled', true);

    }
    else {
        $("#buttonSubmit").prop('disabled', false);
    }
}
function setUserInfo(userId,userName,userEmail,userPhoneNumber,userRole) {
   
    document.getElementById("identityId").value = userId;
    document.getElementById("Name").value = userName;
    document.getElementById("Email").value = userEmail;
    document.getElementById("PhoneNumber").value = userPhoneNumber;
    document.getElementById("Role").value = userRole;

}
function RedirectToAdminIndex() {
    window.location.href = "/Administrator/Index";
}