import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify';
import moment from 'moment';
import { createProvider } from './vue-apollo'

Vue.config.productionTip = false;
Vue.filter('shortDateTime', (d) => moment(d).format('D.M.YYYY H:mm'))

new Vue({
  vuetify,
  apolloProvider: createProvider(),
  render: h => h(App),
}).$mount('#app')
