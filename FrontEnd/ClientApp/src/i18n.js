import i18n from "i18next";
import { initReactI18next } from "react-i18next";

// TODO: Move translations it its own json files
const resources = {
  en: {
    translation: {                  
      "common_loading": "Loading...",
      "menu_home": "Home",
      "menu_hcp": "Hcp",
      "menu_rounds": "Rounds",
      "menu_series": "Series",
      "home_header": "orbiTibro discgolf scores",
      "home_description": "This page is under construction and may change.",
      "hcp_header": "Current handicap",
      "hcp_description_avgscore": "Your last 18 rounds and the best 1/3 of the rounds is used to calculate avverage score. (If you have 4 rounds. It should be based on 1.3333.. rounds. But it is rounded up it is based on 2.)",      
      "hcp_description_hcp": "Hcp = (Average score  - 48) * 0.8.",
      "rounds_header": "Rounds",
      "rounds_description": "The lastest rounds with results.",
      "series_header": "Series",
      "series_description": "Series with results.",
    }
  },
  sv: {
    translation: {      
      "common_loading": "Laddar...",           
      "menu_home": "Hem",
      "menu_hcp": "Hcp",
      "menu_rounds": "Rundor",
      "menu_series": "Serier",
      "home_header": "orbiTibro discgolf resultat",
      "home_description": "Denna site är under konstruktion och förändringar kan ske.",
      "hcp_header": "Nuvarande handicap",
      "hcp_description_avgscore": "Av dina senaste 18 rundor, så tas den bästa 1/3 av rundorna för att beräkna ett snitt av antalet kast. (Men om du bara har 4 rundor. Så görs en avrundning uppåt till att baseras på 2 rundor istället för 1.3333... rundor)",      
      "hcp_description_hcp": "Hcp = (Snitt antal kast  - 48) * 0.8.",
      "rounds_header": "Rundor",
      "rounds_description": "Senaste rundorna med resultat.",
      "series_header": "Serier",
      "series_description": "Serier med results.",
    }
  }
};

i18n
  .use(initReactI18next) // passes i18n down to react-i18next
  .init({
    resources,
    lng: "sv",

    keySeparator: false, // we do not use keys in form messages.welcome

    interpolation: {
      escapeValue: false // react already safes from xss
    }
  }); 

  export default i18n;