import { SIGNUP_ACTION, LOGIN_ACTION, SET_USER_MUTATION } from "@/store/constants";
import api from '../../../axiosConfig';
import Swal from 'sweetalert2'

export default {
    async [LOGIN_ACTION]({ commit }, payload) {
        const data = {
            username: payload.username,
            password: payload.password,
        }
        console.log(data)
        Swal.fire({
            icon: 'info',
            title: "We're trying to authenticate you!",
            html: "This may take a moment...", // add html attribute if you want or remove
            allowOutsideClick: false,
            showConfirmButton: false,
            willOpen: () => {
                Swal.showLoading();
            },
        })
        await api.post(window.$http + "authman/login/", data)
            .then((response) => {
                //console.log(response.data)
                commit(SET_USER_MUTATION, response.data);
                Swal.close()
            }).catch((e) => {
                Swal.fire({
                    title: 'Error',
                    icon: 'danger',
                    html: e,
                    allowOutsideClick: true,
                    showCancelButton: true,
                    timer: 2000,
                })
            })
    },
    async [SIGNUP_ACTION]({ commit }, payload) {
        const data = {
            username: payload.username,
            email: payload.email,
            first_name: payload.first_name,
            last_name: payload.last_name,
            phone: payload.phone,
            password: payload.password,
        }
        console.log(data)
        Swal.fire({
            icon: 'info',
            title: "Pease wait!",
            html: "This may take a moment...", // add html attribute if you want or remove
            allowOutsideClick: false,
            showConfirmButton: false,
            willOpen: () => {
                Swal.showLoading();
            },
        })
        await api.post(window.$http + "authman/register/", data)
            .then((response) => {
                console.log(response.data)
                //commit(SET_USER_MUTATION, response.data);
                Swal.close();
                Swal.fire({
                    title: 'Success!',
                    icon: 'success',
                    html: "User account has been created successfully!",
                    allowOutsideClick: true,
                    showCancelButton: true,
                    timer: 2000,
                });
            }).catch((e) => {
                Swal.fire({
                    title: 'Error',
                    icon: 'danger',
                    html: e,
                    allowOutsideClick: true,
                    showCancelButton: true,
                    timer: 2000,
                })
            })
    }
};