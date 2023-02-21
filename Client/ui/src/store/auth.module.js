import AuthService from '../services/auth.service';

const user = JSON.parse(localStorage.getItem('user'));
const initialState = user ?
    { status: { loggedIn: true }, user } :
    { status: { loggedIn: false }, user: null };

export const auth = {
    namespaced: true,
    state: initialState,
    actions: {
        login({ commit }, user) {
            return AuthService.login(user).then(
                user => {
                    commit('loginSuccess', user);
                    return Promise.resolve(user);
                },
                error => {
                    commit('loginFailure');
                    return Promise.reject(error);
                }
            );
        },
        logout({ commit }) {
            AuthService.logout();
            commit('logout');
        },
        register({ commit }, user) {
            return AuthService.register(user).then(
                response => {
                    commit('registerSuccess');
                    return Promise.resolve(response.data);
                },
                error => {
                    commit('registerFailure');
                    return Promise.reject(error);
                }
            );
        }
    },
    mutations: {
        loginSuccess(state, user) {
            state.status.loggedIn = true;
            state.user = user;
        },
        loginFailure(state) {
            state.status.loggedIn = false;
            state.user = null;
        },
        logout(state) {
            state.status.loggedIn = false;
            state.user = null;
        },
        registerSuccess(state) {
            state.status.loggedIn = false;
        },
        registerFailure(state) {
            state.status.loggedIn = false;
        }
    }
};
export const state = {
    layoutType: 'vertical',
    layoutWidth: 'fluid',
    leftSidebarType: 'light',
    topbar: 'dark',
    loader: false
}

export const getters = {}

export const mutations = {
    CHANGE_LAYOUT(state, layoutType) {
        state.layoutType = layoutType;
    },
    CHANGE_LAYOUT_WIDTH(state, layoutWidth) {
        state.layoutWidth = layoutWidth;
    },
    CHANGE_LEFT_SIDEBAR_TYPE(state, leftSidebarType) {
        state.leftSidebarType = leftSidebarType;
    },
    CHANGE_TOPBAR(state, topbar) {
        state.topbar = topbar;
    },
    LOADER(state, loader) {
        state.loader = loader
    }
}

export const actions = {
    changeLayoutType({ commit }, { layoutType }) {
        commit('CHANGE_LAYOUT', layoutType);
    },

    changeLayoutWidth({ commit }, { layoutWidth }) {
        commit('CHANGE_LAYOUT_WIDTH', layoutWidth)
    },

    changeLeftSidebarType({ commit }, { leftSidebarType }) {
        commit('CHANGE_LEFT_SIDEBAR_TYPE', leftSidebarType)
    },

    changeTopbar({ commit }, { topbar }) {
        commit('CHANGE_TOPBAR', topbar)
    },

    changeLoaderValue({ commit }, { loader }) {
        commit('LOADER', loader)
    }
}