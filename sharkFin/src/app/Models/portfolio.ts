import {Stock} from './stock'

export interface Portfolio {
    id: number,
    name: string,
    funds: number,
    stocks?: Stock[]

}