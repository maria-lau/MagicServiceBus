// To be completed by students in milestone 2

function validateLoginForm() {
    console.log("\n\n\nI'm in the validation checker thing!!!\n\n\n");
    if (loginForm.usernameData.value === "") {
        return false;
    }
    if (loginForm.passwordData.value === "") {
        return false;
    }
    return true;
}

//function validateCAUsernameForm() {
//    if (asIsEchoForm.asIsText.value === "") {
//        return false;
//    }
//    return true;
//}

//function validateCAPasswordForm() {
//    if (asIsEchoForm.asIsText.value === "") {
//        return false;
//    }
//    return true;
//}

//function validateCAAddressForm() {
//    if (asIsEchoForm.asIsText.value === "") {
//        return false;
//    }
//    return true;
//}

//function validateCAEmailForm() {
//    if (asIsEchoForm.asIsText.value === "") {
//        return false;
//    }
//    return true;
//}

//function validateCAPhoneForm() {
//    if (asIsEchoForm.asIsText.value === "") {
//        return false;
//    }
//    return true;
//}