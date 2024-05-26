<template>
    <router-link :to="{name: routerName}" class="btn head-btn2"  @click.native="$emit('click')"
    :style="{backgroundColor: bgColor, color: color, height: height + 'px', lineHeight: (height - 40) + 'px'}">{{content}}</router-link>
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
        color:{
            type: String,
            default: "#fff"
        },
        bgColor:{
            type: String,
            default:"rgb(221, 63, 36)"
        },
        height:{
            type: Number,
            default: 40
        }
    }
}
</script>
<style scoped>
.btn.head-btn2{
    box-sizing: border-box;
    margin: 0 10px;
    padding: 20px 20px;
    border: none;
}
</style>
