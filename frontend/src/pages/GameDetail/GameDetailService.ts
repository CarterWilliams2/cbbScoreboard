
export function formatWinPercent(winProb: number) {

    let percent = 0;
    let percent_string = "50%"

    if (winProb) {
        percent = winProb * 100;
        percent_string = `${percent}%`;
    }
    

    return percent_string;
}