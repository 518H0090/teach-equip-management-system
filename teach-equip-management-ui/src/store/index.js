import { createStore } from 'vuex'

const store = createStore({
    state: {
        is_expanded: localStorage.getItem("is_expanded") || false,
        authenticated: localStorage.getItem("is_authenticated") || false
    },
    mutations: {
        SET_EXPANDED: (state, expanded) =>{
            state.is_expanded = expanded
            localStorage.setItem("is_expanded", state.is_expanded);
        },
        SET_AUTH: (state, auth) => {
            state.authenticated = auth
            localStorage.setItem("is_authenticated", state.authenticated);
        }
    },
    actions: {
        setIsExpanded: ({commit}, expanded) =>  commit('SET_EXPANDED', expanded),
        setAuth: ({commit}, auth) => commit('SET_AUTH', auth)
    },
    modules: {}
})

export default store