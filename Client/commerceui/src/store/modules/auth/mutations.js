import { SET_USER_MUTATION } from "@/store/constants";


export default {
    [SET_USER_MUTATION](state, user) {
        state.user = user;
        console.log(user)
    }
};