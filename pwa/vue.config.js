module.exports = {
  pwa: {
    themeColor: "#4DBA87",
    msTileColor: "#000000",
    appleMobileWebAppCache: "yes",
    manifestOptions: {
      background_color: "#42b983"
    },
    iconPaths: {
      favicon32: 'icon.png',
      favicon16: 'icon.png',
      appleTouchIcon: 'icon.png',
      maskIcon: 'icon.png',
      msTileImage: 'icon.png'
    }
  },
  devServer: {
    https: true,
    disableHostCheck : true
  }
};
