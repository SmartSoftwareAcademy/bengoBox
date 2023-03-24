// store/modules/cart.js
export const state = {
    cart_items: [],
    cart_items_count: 0,
};

export const mutations = {
    addItem(state, product) {
        console.log(product)
        state.cart_items.push(product);
        state.cart_items_count = state.cart_items.length;
    }
};

export const actions = {
    addProductToCart({ commit }, product) {
        console.log(product)
        commit('addItem', product);
    }
};

// export default {
//     namespaced: true,
//     state,
//     mutations,
//     actions
// };
