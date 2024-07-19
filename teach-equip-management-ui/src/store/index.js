import { createStore } from 'vuex'

const store = createStore({
    state: {
        is_expanded: localStorage.getItem("is_expanded")
    },
    mutations: {
        SET_EXPANDED: (state, expanded) =>{
            state.is_expanded = expanded
            localStorage.setItem("is_expanded", state.is_expanded);
        } 
    },
    actions: {
        setIsExpanded: ({commit}, expanded) =>  commit('SET_EXPANDED', expanded)
    },
    modules: {}
})

export default store