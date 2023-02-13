# Publish life cycle

## Update routing
VS -> Tools -> Version changer
cmd: `git add *.csproj && git add *AssemblyInfo.cs && git add CHANGELOG.md && git cism "#Version update to v0.x.xxx" && git bra && git st`
cmd: `git stad && git cism "#Version update to v0.x.xxx" && git stad`

## Tag routing
cmd: `git tag -a v0.x.xxx <commit_hash> && git push all --tags`

## Branche routing
- view changes:       `cls && git bra && git st`
- add & view changes: `git stad`
- develop -> preview: `git switch preview && git merge develop && git push all preview && git switch develop && git bra && git st`
- bagger -> preview:  `git switch bagger && git pull all bagger && git switch preview && git merge bagger && git bra && git st`
- preview -> develop: `git switch develop && git merge preview && git bra && git st`
- preview -> main:    `git switch main && git merge preview && git push all main && git switch preview && git bra && git st`
- develop -> main:    `git switch main && git merge develop && git push all main && git switch develop && git bra && git st`
- rollback merge:     `git merge --quit && git reset . && git checkout . && git clean -fd && cls && git bra && git st`

## Release/Preview ScalesUI routing
Временное окно выпуска обновления ПО "Печать этикеток": 16.30 - 17.00.
ПО "Печать этикеток" обновлена до версии v0.x.yyy.
Publish ScalesUI preview: `\\palych\Install\VSSoft\Scales-2-Preview\`
Publish ScalesUI release: `\\palych\Install\VSSoft\Scales-3-Release\`

## Release/Preview DeviceControl routing
Publish DeviceControl dev-preview:  `https://device-control-dev-preview.kolbasa-vs.local/`
Publish DeviceControl dev-release:  `https://device-control-dev.kolbasa-vs.local/`
Publish DeviceControl prod-preview: `https://device-control-prod-preview.kolbasa-vs.local/`
Publish DeviceControl prod-release: `https://device-control.kolbasa-vs.local/`
