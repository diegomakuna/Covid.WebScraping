export default class Helper {
  static toNumberFormat(number?: number) {
    return number
      ? number?.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".")
      : "--";
  }
}