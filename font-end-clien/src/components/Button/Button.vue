<template>
    <router-link :to="{name: routerName}" class="btn head-btn2" @click.native="$emit('click')">{{content}}</router-link>
</template>

<script>
export default {
    name: 'Button',
    props:{
        routerName:{
            type:String,
            default: '',
        },  
        content:{
            type: String,
            default: '',
        },
    },
    methods:{
    }
}
</script>
<style scoped>
.btn.head-btn2{
    margin: 0 10px;
    padding: 20px 20px;
}
</style>
