import Vue from 'vue'
import App from './App.vue'
import router from './router'
import ApolloClient from 'apollo-boost'
import VueApollo from 'vue-apollo'

Vue.config.productionTip = false

const apolloClient = new ApolloClient({
  // You should use an absolute URL here
  uri: 'https://localhost:5001/graphql'
})

const apolloProvider = new VueApollo({
  defaultClient: apolloClient
})

Vue.use(VueApollo)

new Vue({
  router,
  apolloProvider,
  render: h => h(App)
}).$mount('#app')
