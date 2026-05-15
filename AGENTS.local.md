# Lokala instruktioner för Codex

Den här filen är avsedd för lokala, projektunika arbetsregler som inte ska checkas in.

När du jobbar i projektet:

- Läs den här filen först och följ den om inget annat sägs i chatten.
- Fråga alltid användaren om lov innan du gör några kodändringar, även små ändringar.
- Fråga innan du gör större strukturändringar, databasändringar eller tar bort kod.
- Behåll befintlig kodstil och namngivning om det inte finns ett tydligt skäl att ändra.
- Gör små, fokuserade ändringar hellre än breda refaktoriseringar.
- Förklara kort vad som ändrats och hur det verifierades.

Plats för egna regler:

- Språk i kodkommentarer: engelska
- Språk i UI: engelska
- Teststrategi:
- Filer/mappar som inte får ändras utan att fråga:
- Övriga preferenser:
  - Jag är nybörjare och vill ha tydliga, pedagogiska svar.
  - Hjälp mig med struktur, arkitektur och kodning.
  - Gör komplexa resonemang enklare att förstå genom att bryta ner dem steg för steg.
  - Förklara varje viktigt steg i koden: vad koden gör och varför den behövs.
  - Ge inte bara kod. Det är mycket viktigt att jag förstår exakt vad koden gör och varför.
  - När du föreslår kodlösningar, lägg in tydliga kommentarer i koden som förklarar de viktiga delarna.
  - Använd inte `var` i C#-kod. Skriv ut datatypen tydligt så att det är lättare att förstå vilken typ varje variabel har.

## Git / commits

- Gör aldrig commit utan att fråga.
- Visa alltid en kort sammanfattning innan staging.
- Använd commit-meddelanden i formatet: `feat: kort beskrivning i imperativ-form`
- Skapa inte nya branches utan att fråga först.
