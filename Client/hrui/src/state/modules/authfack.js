import { userService } from '../../helpers/bengoboxAuth/user.service';
import router from '../../router/index'
import Swal from "sweetalert2";

const user = JSON.parse(localStorage.getItem('user'));
export const state = user
    ? { status: { loggeduser: true }, user }
    : { status: {}, user: null };

export const actions = {
    login({ dispatch, commit }, { email, password }) {
        commit('loginRequest', { email });
        Swal.fire({
            title: "Please Wait !",
            html: "We are trying to authenticate you...", // add html attribute if you want or remove
            allowOutsideClick: false,
            showConfirmButton: false,
            willOpen: () => {
                Swal.showLoading();
            },
        });
        userService.login(email, password)
            .then(
                user => {
                    commit('loginSuccess', user);
                    Swal.close();
                    router.push('/');
                },
                error => {
                    commit('loginFailure', error);
                    dispatch('notification/error', error, { root: true });
                }
            );
    },
    // Logout the user
    logout({ commit }) {
        userService.logout();
        commit('logout');
    },
    // register the user
    registeruser({ dispatch, commit }, user) {
        commit('registerRequest', user);
        userService.register(user)
            .then(
                user => {
                    commit('registerSuccess', user);
                    dispatch('notification/success', 'Registration successful', { root: true });
                    router.push('/login');
                },
                error => {
                    commit('registerFailure', error);
                    dispatch('notification/error', error, { root: true });
                }
            );
    }
};

export const mutations = {
    loginRequest(state, user) {
        state.status = { loggingIn: true };
        state.user = user;
    },
    loginSuccess(state, user) {
        state.status = { loggeduser: true };
        state.user = user;
    },
    loginFailure(state) {
        state.status = {};
        state.user = null;
    },
    logout(state) {
        state.status = {};
        state.user = null;
    },
    registerRequest(state) {
        state.status = { registering: true };
    },
    registerSuccess(state) {
        state.status = {};
    },
    registerFailure(state) {
        state.status = {};
    }
};

