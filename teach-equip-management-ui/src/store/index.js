import { createStore } from 'vuex'

const store = createStore({
    state: {
        is_expanded: localStorage.getItem("is_expanded") || false,
        authenticated: localStorage.getItem("is_authenticated") || false,
        request_return_item: {}
    },
    mutations: {
        SET_EXPANDED: (state, expanded) =>{
            state.is_expanded = expanded
            localStorage.setItem("is_expanded", state.is_expanded);
        },
        SET_AUTH: (state, auth) => {
            state.authenticated = auth
            localStorage.setItem("is_authenticated", state.authenticated);
        },
        SET_RETQUEST_RETURN: (state, request_return) => state.request_return_item = request_return
    },
    actions: {
        setIsExpanded: ({commit}, expanded) =>  commit('SET_EXPANDED', expanded),
        setAuth: ({commit}, auth) => commit('SET_AUTH', auth),
        setRequestReturn: ({commit}, request_return) =>  commit('SET_RETQUEST_RETURN', request_return)
    },
    modules: {},
})

export default store