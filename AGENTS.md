# THCS Agent Operating Guide

이 파일은 저장소 전체에 적용된다. 사용자의 현재 요청이 항상 최우선이며, 아래 규칙은 요청을 안전하고 일관되게 수행하기 위한 기본값이다.

## Project facts

- Unity: `6000.3.9f1`
- 렌더링: 2D URP
- 프로젝트 코드와 콘텐츠: `Assets/0.THCS/`
- 주요 씬: `Title`, `Loading`, `MainMenu`, `GamePlay`, `GameUI`
- 주요 구조: persistent/scene Singleton, Observer events, ScriptableObject 데이터, Addressables, object pooling, additive `GameUI` scene, 로컬 JSON 저장
- 외부 자산: `Assets/ExternalAssets/`, `Assets/Plugins/` 및 기타 서드파티 폴더
- 테스트 프레임워크는 설치되어 있지만 현재 프로젝트 테스트/asmdef 기반은 없다.

## Default operating model

사용자는 기본적으로 Lead와 대화한다. Lead는 요청의 범위와 위험에 따라 필요한 역할만 호출하며, 모든 작업에 모든 Agent를 호출하지 않는다.

1. Lead가 요청, 성공 조건, 변경 범위를 정리한다.
2. 여러 시스템이나 수명 주기에 영향을 주면 Architect가 먼저 경계를 설계한다.
3. C# 동작 구현은 Gameplay Implementer가 담당한다.
4. Scene, Prefab, ScriptableObject, Addressables 설정은 Unity Integration Agent가 담당한다.
5. Reviewer는 변경을 독립적으로 검토한다.
6. Test & Performance Agent는 위험에 비례해 테스트와 런타임 검증을 수행한다.
7. Lead가 결과를 통합하고 사용자에게 하나의 결론으로 보고한다.

작고 국소적인 변경은 Lead 또는 Implementer가 직접 처리할 수 있다. 서로 독립적인 조사나 변경만 병렬화하고, 같은 파일을 동시에 수정하지 않는다.

## Agent definitions

- Lead / Coordinator: `.agents/lead.md`
- Architect: `.agents/architect.md`
- Gameplay Implementer: `.agents/gameplay-implementer.md`
- Unity Integration Agent: `.agents/unity-integration.md`
- Reviewer: `.agents/reviewer.md`
- Test & Performance Agent: `.agents/test-performance.md`

역할을 위임할 때 해당 파일을 전부 읽고, 요청에 필요한 프로젝트 파일도 직접 확인한다. 역할 파일은 지식의 대체물이 아니라 책임과 판단 기준이다.

## Agent model routing

역할을 위임할 때 아래 모델과 추론 단계를 기본값으로 사용한다. 사용자가 특정 작업에 다른 모델 또는 추론 단계를 명시하면 사용자 지시를 우선한다.

- Lead / Coordinator: `gpt-5.6-sol`, `low` (Sol-Light)
- Architect: `gpt-5.6-sol`, `low` (Sol-Light)
- Reviewer: `gpt-5.6-sol`, `low` (Sol-Light)
- Gameplay Implementer: `gpt-5.6-terra`, `medium` (Terra-Medium)
- Unity Integration Agent: `gpt-5.6-terra`, `medium` (Terra-Medium)
- Test & Performance Agent: `gpt-5.6-terra`, `medium` (Terra-Medium)

Lead, Architect, Reviewer는 설계 판단과 독립 검토를 담당하므로 Sol-Light를 사용한다. 구현, Unity 연결, 테스트 실행은 Terra-Medium을 사용하고, 고위험 결함 분석이나 구조 변경처럼 판단 비중이 큰 경우에만 Lead가 Sol-Light 위임을 선택한다.

## Repository rules

- `Assets/0.THCS/`를 first-party 영역으로 취급한다.
- 사용자가 명시하지 않는 한 `Assets/ExternalAssets/`, `Assets/Plugins/`, `Library/`, `Logs/`, `obj/`, `UserSettings/`를 수정하지 않는다.
- Unity가 생성한 `.meta` GUID를 보존한다. 자산 이동/이름 변경은 대응하는 `.meta`와 참조 관계까지 고려한다.
- Scene/Prefab YAML을 텍스트로 직접 수정하지 않는다. 가능하면 Unity Editor 또는 Unity MCP를 사용하고, 변경 후 Console과 직렬화 결과를 확인한다.
- `[SerializeField]` 필드 이름/타입 변경은 기존 Scene/Prefab 데이터 호환성을 검토한다. 필요한 경우 `FormerlySerializedAs` 또는 명시적 마이그레이션을 사용한다.
- 새 패키지나 프레임워크는 기존 Unity 기능으로 해결하기 어렵고 이득이 명확한 경우에만 제안한다.
- 외부 자산 코드를 고쳐야 한다면 먼저 wrapper/adapter로 격리할 수 있는지 검토한다.
- 작업 전 관련 파일과 호출 경로를 읽고, 사용자가 만든 관련 없는 변경은 보존한다.

## THCS-specific correctness checks

- Singleton의 persistent lifetime과 `SceneSingleton`의 scene lifetime을 혼동하지 않는다.
- Observer/event 구독은 `OnEnable`/`OnDisable` 또는 동일한 수명 주기에서 대칭적으로 해제한다.
- additive `GameUI` 로딩 시 중복 로드, unload, 참조 소유권, `Time.timeScale` 복구를 확인한다.
- Addressables 비동기 작업 완료 전 초기화 완료로 표시하지 않는다. handle 소유권과 release 시점을 명확히 한다.
- Pool에서 재사용되는 객체는 health, crowd control, animation trigger, event subscription, transform 상태를 초기화한다.
- ScriptableObject 원본 데이터를 런타임 진행 상태로 직접 변형하지 않는다.
- 저장 데이터 변경은 기존 파일을 읽을 수 있는지, 누락 필드의 기본값이 안전한지 확인한다.
- 프레임 반복 경로(`Update`, `FixedUpdate`, target search, spawn)에 불필요한 allocation과 전역 탐색을 추가하지 않는다.
- 사용자에게 검증했다고 말하려면 실제로 수행한 compile/test/Play Mode 확인과 결과를 구분해 보고한다.

## Decisions and handoff

여러 기능에 영향을 주거나 이후 작업이 따라야 하는 결정을 내렸다면 최종 보고에 다음을 남긴다.

- 결정과 이유
- 영향을 받는 Scene/Prefab/ScriptableObject/C# 경계
- 대안과 주요 trade-off
- 남은 위험 또는 검증하지 못한 항목
