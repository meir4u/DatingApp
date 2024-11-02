export const environment = {
  production: true,
  applicationName: "DattingApp",
  applicationHeader: {
    title: "Dating App",
    tags: {
      description: 'This is a description of DatingApp',
      author: "Meir Achildiev",
      keywords: 'dating, social, app'
    },
  },
  apiUrl: 'api/',
  hubUrl: 'hubs/',
  google: {
    client_id: "821510797455-mb3hi7d1udvtdg9l43h6rjs36kp999qn.apps.googleusercontent.com",
    redirect_uri: "https://dattingapp-app.piemei.easypanel.host/google/auth/callback/"
  },
  email: {
    support: "support",
    domain: "dattingapp-app.piemei.easypanel.host"
  },
  plausible: {
    domain: "dattingapp-app.piemei.easypanel.host",
    src: "https://dattingapp-plausible.piemei.easypanel.host/js/script.js"
  }

};
