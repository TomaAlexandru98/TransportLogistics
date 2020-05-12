function loadServerPartialView(container, baseUrl) {
  return  $.get(baseUrl, function (responseData) {
        $(container).html(responseData);
    });
}
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function Validate() {
    var correctForm = true;
  
    if (document.getElementById("Name").value == "") {
        
         correctForm = false;
    }
   
    
    if (document.getElementById("Email").value == "") {
       
        correctForm = false;
    }
   
  
    if (document.getElementById("PhoneNumber").value == "") {
      
        correctForm = false;
    }
   
    if (correctForm == false) {
         
        $("#EditBtn").prop('disabled', true);
        

    }
    else {
        $("#EditBtn").prop('disabled', false);
       
    }
}
function setUserInfo(userId,userName,userEmail,userPhoneNumber,userRole) {
   
    document.getElementById("identityId").value = userId;
    document.getElementById("name").value = userName;
    document.getElementById("email").value = userEmail;
    document.getElementById("phoneNumber").value = userPhoneNumber;
    document.getElementById("Role").value = userRole;

}
function RedirectToAdminIndex() {
    window.location.href = "/Administrator/Index";
}
function sendUserId(id){
    document.getElementById('userToDeleteId').value = id;
}
function DeleteUserAccount(Id) {
    loadServerPartialView("#removeModal", '@Url.Action("DeleteUserAccount","Administrator")' + "/" + Id);
   

}