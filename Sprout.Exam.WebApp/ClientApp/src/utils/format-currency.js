export default function formatCurrency(value) {
  //https://stackoverflow.com/questions/149055/how-to-format-numbers-as-currency-strings
  return value.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
}
