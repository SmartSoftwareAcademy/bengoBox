export default class validations {
    static checkEmail(email) {
        if (/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email,)) {
            return true;
        }
        return false;
    }

    static checkPhone(phone, minLength) {
        if (phone.lenght < minLength) {
            return false;
        }
        return true;
    }

    static checkPassword(password, newpassword, minLength) {
        if (password.length < minLength || newpassword !== password) {
            return false;
        }
        return true;
    }
}