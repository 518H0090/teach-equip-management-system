import { createStore } from 'vuex'

const store = createStore({
    state: {
        is_expanded: false
    },
    mutations: {
        SET_EXPANDED: (state, expanded) => state.is_expanded = expanded
    },
    actions: {
        setIsExpanded: ({commit}, expanded) =>  commit('SET_EXPANDED', expanded)
    },
    modules: {}
})

export default store