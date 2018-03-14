// To be completed by students in milestone 2

function validateLoginForm() {
    //console.log("\n\n\nI'm in the validation checker thing!!!\n\n\n");
    if (loginForm.usernameData.value === "") {
        return false;
    }
    if (loginForm.passwordData.value === "") {
        return false;
    }
    return true;
}

function validateCreateAccountForm() {
    if (createAccountForm.usernameData.value === "") {
        return false;
    }
    return true;
}
