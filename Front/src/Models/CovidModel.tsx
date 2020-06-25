import {AreaModel} from "./AreaModel"
export type CovidModel = {
    _id?: number
    create?: string
    confirmed?: number,
    active?: number
    recovered?: number;
    deaths?: number;
    areas?: AreaModel[];
}