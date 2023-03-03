import validations from "./validations";

export default class signupvalidations {
    constructor(email, password, newpassword, phone) {
        this.email = email;
        this.password = password;
        this.newpassword = newpassword;
        this.phone = phone;
    }
    checkValidations() {
        let errors = [];
        //email check
        if (!validations.checkEmail(this.email)) {
            errors['email'] = 'Inalid Email';
        }
        //password check
        if (!validations.checkPassword(this.password, this.newpassword, 8)) {
            errors['password'] = 'Password Must contain at least 8 characters. Passwords must also match!';
        }
        if (!validations.checkPhone(this.phone, 10)) {
            errors['phone'] = 'Phone Number Must contain at least 10 digits.';
        }
        return errors;
    }
}