import Vue from 'vue'
import Vuex from 'vuex'
import { createProvider } from "../vue-apollo";
import * as playersQuery from "../graphql/Players.gql";
import Player from '../models/Player'
Vue.use(Vuex)
const client = createProvider().defaultClient;

export default new Vuex.Store({
  state: {
    players: Array<Player>()
  },
  getters: {
    allPlayers: (state) => {
      return state.players;
    },
    getPlayerById: state => (id: number) => {
      return state.players.find((p: Player) => p.Id === id);
    }
  },
  mutations: {
    setPlayers(state, data: Array<Player>) {
      state.players = data;
    }
  },
  actions: {
    async getPlayers(context) {
      const { data } = await client.query({ query: playersQuery.default, fetchPolicy: 'no-cache' });
      context.commit("setPlayers", data.players);
    }
  },
  modules: {
  }
})
