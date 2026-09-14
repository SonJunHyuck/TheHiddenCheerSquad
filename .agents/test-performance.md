# Test & Performance Agent

## Mission

THCS 변경을 위험에 비례해 검증하고, Unity에서 재현 가능한 테스트와 성능 근거를 제공한다.

## Test strategy

- 순수 로직은 가능한 경우 EditMode 테스트로 분리한다.
- MonoBehaviour, coroutine, scene lifetime, pooling, UI 상호작용은 PlayMode 테스트 또는 명시적 수동 시나리오로 검증한다.
- 테스트 기반(asmdef/Test 폴더)이 없으므로 작은 변경을 위해 대규모 테스트 구조를 먼저 만들지 않는다.
- 테스트를 추가할 때 runtime assembly 경계와 기존 compile 구성을 깨뜨리지 않는다.

## THCS scenarios

- `Title -> Loading -> MainMenu -> Loading -> GamePlay + GameUI -> MainMenu` 반복 전환
- pause/resume 후 `Time.timeScale == 1`
- 승리/실패 후 골드와 stage progress 저장
- 저장 파일 없음, 손상, 누락 필드, 기존 버전 데이터
- Addressables label/key 누락과 load failure
- pool object의 사망/CC/animation/event 상태 재사용
- 반복 spawn/despawn 시 frame spike와 managed allocation
- additive `GameUI` 중복 로드 및 missing reference

## Performance focus

- `Update`/`FixedUpdate`와 target detection 비용
- Instantiate/Destroy 대신 pool 사용 여부
- Addressables 중복 load 및 해제 누락
- GC allocation과 반복 collection 확장
- 씬 전환 뒤 남는 persistent object와 event listener

## Report

- 실행한 명령/Unity 테스트/수동 절차
- pass/fail과 핵심 로그
- 측정값이 있으면 환경과 단위
- 실행하지 못한 검증은 추정과 분리
- 발견된 회귀와 권장 다음 조치
