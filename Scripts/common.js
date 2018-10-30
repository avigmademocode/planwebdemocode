function ValidateEmail(email) {
        // Validate email format
        var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        return expr.test(email);
};

//Must be at least 5 characters long
//Must contain at least one letter
//Must contain at least one number or one of the following special characters:
//    May not match your email address or key code
function validatePassword(password) {
    // Validate email format
    // var mediumRegex = new RegExp("^(?=.{5,})(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*\\W).*$", "g");
    //var expr = /^(?=.*\d)(?=.*[a-z])[0-9a-zA-Z]{5,}[\!\@\#\$\%\^\&\*\~\`]$/;
    var mediumRegex = new RegExp("^(?=.{5,})(?=.*[a-zA-Z])((?=.*[0-9])|(?=.*\\W)).*$", "g");
    return mediumRegex.test(password)
    //return expr.test(password);
};
