import { createStore } from 'vuex'
import auth from './modules/auth/index';

export default createStore({
  state: {
    sidebarVisible: '',
    sidebarUnfoldable: false,
    user: {},
  },
  mutations: {
    toggleSidebar(state) {
      state.sidebarVisible = !state.sidebarVisible
    },
    toggleUnfoldable(state) {
      state.sidebarUnfoldable = !state.sidebarUnfoldable
    },
    updateSidebarVisible(state, payload) {
      state.sidebarVisible = payload.value
    },
  },
  actions: {},
  modules: {
    auth,
  },
})
