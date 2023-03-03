import actions from './actions';
import getters from './getters';
import mutations from './mutations'

export default {
    namespaced: true,
    state() {
        return {
            name: "bengobox store"
        }
    },
    mutations: mutations,
    actions: actions,
    getters: getters,
}