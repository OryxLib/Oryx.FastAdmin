<template>
  <iframe src="https://localhost:44391/fd#/" width="100%" height="100%" @load="resizeIframe"/>
</template>

<script>
export default {
  name: "frontEndBuilder",
  data() {
    return {};
  },
  created() {},
  methods: {
    resizeIframe(obj) {
      console.log(obj);
      obj.target.style.height = window.innerHeight + "px";
      // obj.target.style.height = obj.target.contentWindow.document.body.scrollHeight + 'px'
    }
  }
};
</script>


<style scoped>
.chart-container {
  position: relative;
  width: 100%;
  height: calc(100vh - 84px);
}
iframe {
  border: none;
}
</style>
