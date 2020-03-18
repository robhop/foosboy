import Vue from 'vue'
import Vuex from 'vuex'
import gql from 'graphql-tag'
import { createProvider } from '../vue-apollo'


const provider = createProvider();

console.dir(provider);
Vue.use(Vuex)

const actions = {
  async getPlayers({ commit }) {
    console.time('getLanguages')

    const response = await provider.defaultClient.query({
      query: gql`
      query { players {

        name 
      }}
      `
    })

    const { players } = response.data
    commit('SET_PLAYERS', { players })

    console.timeEnd('getLanguages')
  }
}

const mutations = {
  SET_PLAYERS: (state, { players }) => {
    state.players = players;
  }

}

export default new Vuex.Store({
  state: {
    players: []
  },
  mutations: mutations,
  actions: actions,
  modules: {
  }
})
