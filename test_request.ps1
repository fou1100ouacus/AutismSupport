# Test script for ChildProfile API
$headers = @{
    "Content-Type" = "application/json"
    "Authorization" = "Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYXlhMSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiYXlhMSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6Im15b3lhNjI5QGdtYWlsLmNvbSIsIlBob25lTnVtYmVyIjoiMDEwOTg3NjU0MzIiLCJJZCI6IjkiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJVc2VyIiwiZXhwIjoxNzc2NzE2MTM1LCJpc3MiOiJTY2hvb2xQcm9qZWN0IiwiYXVkIjoiV2ViU2l0ZSJ9.FJp9aVH1pq_K16pMhaocojlPvk9TSNfuVtZP9sbTpKg"
}

$body = @{
    nickname = "Ahmed Ali"
    ageInYears = 5
    ageInMonths = 3
    gender = 1
    supportNeedsLevel = 2
    mainDailyChallengesJson = '["sensory sensitivities", "sleep", "communication"]'
    strengthsAndInterests = "Drawing, music, and playing with cars"
    prefersVisualSchedules = $true
    communicationMethodsJson = '["PECS", "Speech", "Tablet AAC"]'
}

$jsonBody = $body | ConvertTo-Json

try {
    $response = Invoke-RestMethod -Uri "https://localhost:5167/api/ChildProfile" -Method POST -Headers $headers -Body $jsonBody -SkipCertificateCheck
    Write-Host "Success:"
    $response | ConvertTo-Json -Depth 10
} catch {
    Write-Host "Error occurred:"
    Write-Host "Status Code: $($_.Exception.Response.StatusCode)"
    $reader = [System.IO.StreamReader]::new($_.Exception.Response.GetResponseStream())
    $errorResponse = $reader.ReadToEnd()
    $reader.Close()
    Write-Host "Error Response: $errorResponse"
}
