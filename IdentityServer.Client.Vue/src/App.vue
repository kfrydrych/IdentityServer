<template>
    <div id="app">
        <Home msg="Hello world!" />
        <button @click="login">Login</button>
        <button @click="logout">Logout</button>
    </div>
</template>

<script>
    import Mgr from './services/SecurityService'
    import Home from './components/Home.vue';

    export default {
        name: 'app',
        components: {
            Home
        },
        created(){
           //this.mgr.getSignedIn()
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
            logout(){
                this.mgr.signOut()
            }
        }      
    };
</script>

<style>
</style>
