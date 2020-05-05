// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function Validate() {
    var correctForm = true;
    var name = document.getElementById("Name");
    if (name.value == "") {
        document.getElementById("LabelName").value = 'The name is required';
        document.getElementById("LabelName").style.color = "red";
        //name.value =
        correctForm = false;
    }
    else {
        document.getElementById("LabelName").value = "Name";
        document.getElementById("LabelName").style.color = "white";
    }
    if (document.getElementById("Email").value == "") {
        document.getElementById("LabelEmail").value = "The email is required";
        document.getElementById("LabelEmail").style.color = "red";
        correctForm = false;
    }
    else  {
        document.getElementById("LabelName").value = "Email";
        document.getElementById("LabelName").style.color = "white";
    }
    if (document.getElementById("Role").value == "") {
        document.getElementById("LabelRole").value = "The role is required";
        document.getElementById("LabelRole").style.color = "red";
        correctForm = false;
    }
    else {
        document.getElementById("LabelName").value = "Role";
        document.getElementById("LabelName").style.color = "white";
    }
    if (document.getElementById("PhoneNumber").value == "") {
        document.getElementById("LabelPhoneNumber").value = "The phone number is required";
        document.getElementById("LabelPhoneNumber").style.color = "red";
        correctForm = false;
    }
    else {
        document.getElementById("LabelName").value = "Phone Number";
        document.getElementById("LabelName").style.color = "white";
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