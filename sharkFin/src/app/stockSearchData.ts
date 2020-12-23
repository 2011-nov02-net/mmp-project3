export interface stockSearch {
    
        name: string,
        exchange: string,
        ticker: string,
        industry: string,
        price: number
    
}
    <h4>{{searchRes.name}} - {{searchPrice.c}}</h4>
    <h6>{{searchRes.exchange}}</h6>
    <h6>{{searchRes.ticker}} - {{searchRes.finnhubIndustry}}</h6>
} 