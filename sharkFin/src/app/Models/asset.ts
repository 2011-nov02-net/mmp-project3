import {Stock} from './stock'

export interface Asset {
    id?: number,
    quantity: number,
    stock: Stock,
    price?: number

}