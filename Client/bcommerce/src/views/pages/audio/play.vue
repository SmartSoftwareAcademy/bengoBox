<template>
    <div>
        <audio ref="myAudio" :src="audioSrc"></audio>
        <button @click="playAudio">Play</button>
        <button @click="pauseAudio">Pause</button>
    </div>
</template>

<script>
export default {
    data() {
        return {
            audioSrc: require('@/assets/audio/forward.mp3'),
        };
    },
    methods: {
        playAudio() {
            this.$refs.myAudio.play();
        },
        pauseAudio() {
            this.$refs.myAudio.pause();
        },
    },
};
</script>
