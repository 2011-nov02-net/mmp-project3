import {Stock} from './stock'
import {Asset} from './asset'
import {Trade} from './trade'

export interface Portfolio {
    id: number,
    name: string,
    funds: number,
    assets?: Asset[],
    trades?: Trade[]


}