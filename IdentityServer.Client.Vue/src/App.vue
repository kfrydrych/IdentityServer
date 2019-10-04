<template>
    <div id="app">
        <Home msg="Hello world!" />
        <button @click="login">Login</button>
        <button @click="getSmarphones">Show Smartphones</button>
        <button @click="getSmarphonesStock">Show Smartphones And Current Stock</button>
        <button @click="logout">Logout</button>
    </div>
</template>

<script>
    import Mgr from './services/SecurityService'
    import Home from './components/Home.vue'
    import axios from 'axios'

    export default {
        name: 'app',
        components: {
            Home
        },
        created(){
           this.mgr.getSignedIn()
        },
        data () {
            return {
                mgr: new Mgr()
            }    
        },
        methods: {
            login(){
                this.mgr.signIn();
            },
            async getSmarphones(){

                const token = await this.mgr.getAcessToken();    

                const config = {
                    headers: {'Authorization': "bearer " + token}
                };

                const result = await axios.get("https://localhost:44337/api/smartphones", config)
                const smarphones = result.data;
                console.log(smarphones);
            },
            async getSmarphonesStock(){

                const token = await this.mgr.getAcessToken();    

                const config = {
                    headers: {'Authorization': "bearer " + token}
                };

                const result = await axios.get("https://localhost:44354/api/search", config)
                const inventoryRaport = result.data;
                console.log(inventoryRaport);
            },
            logout(){
                this.mgr.signOut()
            }
        }      
    };
</script>

<style>
</style>
