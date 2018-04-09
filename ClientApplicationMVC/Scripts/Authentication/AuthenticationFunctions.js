function validateCreateAccountForm() {
    var emailRe = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (!emailRe.test(String(createAccountForm.emailData.value).toLowerCase())) {
        alert("Please enter a valid e-mail.");
        return false;
    }
    var phoneRe = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im;
    if (!phoneRe.test(String(createAccountForm.phoneData.value).toLowerCase())) {
        alert("Please enter a valid phone number with no spaces or dashes.");
        return false;
    }
    return true;
}
