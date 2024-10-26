export const environment = {
  production: false,
  applicationName: "Dating App",
  //apiUrl: 'https://localhost:7269/api/',
  //hubUrl: 'https://localhost:7269/hubs/',
  apiUrl: 'https://localhost:44387/api/',
  hubUrl: 'https://localhost:44387/hubs/',
  google: {
    client_id: "821510797455-mb3hi7d1udvtdg9l43h6rjs36kp999qn.apps.googleusercontent.com",
    redirect_uri: "http://localhost:44387/google/auth/callback/"
  },
  email: {
    support: "support",
    domain: "localhost:44387"
  }
};
